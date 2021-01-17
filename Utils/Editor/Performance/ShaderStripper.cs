//#define SHADER_COMPILATION_LOGGING
//#define SKIP_SHADER_COMPILATION

using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace AnilTools.AnilEditor
{
    public class ShaderStripper : IPreprocessShaders
    {

        private const string LOG_FILE_PATH = "Library/Shader Compilation Results.txt";

        private static readonly ShaderKeyword[] SKIPPED_VARIANTS = new ShaderKeyword[]
        {
        new ShaderKeyword( "DIRECTIONAL_COOKIE" ),
        new ShaderKeyword( "POINT_COOKIE" ),
        new ShaderKeyword( "LIGHTPROBE_SH" ), // Apparently used by lightmapping, as well
        };

        public int callbackOrder { get { return 0; } }

        public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
        {
            // Don't strip essential shaders
            string shaderName = shader.name;
            if (shaderName.StartsWith("Unlit/") || shaderName.StartsWith("Particles/"))
                return;

#if SHADER_COMPILATION_LOGGING
		System.IO.File.AppendAllText( LOG_FILE_PATH, "\n\n\n\n===== " + shader.name + " " + snippet.passName + " " + snippet.passType + " " + snippet.shaderType + "\n" );
#endif

            if (snippet.passType == PassType.Deferred || snippet.passType == PassType.LightPrePassBase || snippet.passType == PassType.LightPrePassFinal || snippet.passType == PassType.ScriptableRenderPipeline || snippet.passType == PassType.ScriptableRenderPipelineDefaultUnlit)
            {
#if SHADER_COMPILATION_LOGGING
			System.IO.File.AppendAllText( LOG_FILE_PATH, "Skipped shader variant because it uses SRP or Deferred shading\n" );
#endif

                data.Clear();
            }

            for (int i = data.Count - 1; i >= 0; --i)
            {
                bool shouldSkipShaderVariant = false;
                foreach (ShaderKeyword keywordToSkip in SKIPPED_VARIANTS)
                {
                    if (data[i].shaderKeywordSet.IsEnabled(keywordToSkip))
                    {
                        shouldSkipShaderVariant = true;
                        break;
                    }
                }

                if (shouldSkipShaderVariant)
                {
                    data.RemoveAt(i);
                    continue;
                }

#if SHADER_COMPILATION_LOGGING
			string keywords = "";
			foreach( ShaderKeyword keyword in data[i].shaderKeywordSet.GetShaderKeywords() )
				keywords += keyword.GetKeywordName() + " ";

			if( keywords.Length == 0 )
				keywords = "No keywords defined";

			System.IO.File.AppendAllText( LOG_FILE_PATH, "- " + keywords + "\n" );
#endif
            }

#if SKIP_SHADER_COMPILATION
		for( int i = data.Count - 1; i >= 0; --i )
			data.Clear();
#endif
        }
    }
}