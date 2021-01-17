using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnilTools
{
    public static class TextExtentions
    {
        // usage example:
        // StartCoroutine(Text.TextFadeAnim("your text here"));

        private const char Space = ' '; // mem allocation

        private static WaitForSecondsRealtime TextUpdateTime = new WaitForSecondsRealtime(0.01f);

        // Text Extentions
        public static IEnumerator TextLoadingAnim(this Text text, string willBeText)
        {
            text.text = string.Empty;
            foreach (var Char in willBeText.ToCharArray()){
                text.text += Char;
                yield return TextUpdateTime;
            }
        }

        /// <summary>
        /// this allows you to fade Text anim
        /// </summary>
        /// <returns>Nothing</returns>
        public static IEnumerator TextFadeAnim(this Text text, string willBeText)
        {
            List<TextData> textDatas = new List<TextData>();
            StringBuilder stringBuilder = new StringBuilder(text.text.Length);
            text.text = string.Empty;

            for (short i = 0; i < text.text.Length; i++){
                text.text += Space;
                stringBuilder[i] = Space;
            }

            for (short i = 0; i < willBeText.Length; i++){
                textDatas.Add(new TextData(willBeText[i],i));
            }

            while (textDatas.Count > 0){
                var textData = textDatas[RandomReal.Range(0,textDatas.Count)];
                stringBuilder[textData.id] = textData._char;
                text.text = stringBuilder.ToString();
                textDatas.Remove(textData);
                yield return TextUpdateTime;
            }
        }

        /// <summary>
        /// this allows you to Disolve Text anim
        /// </summary>
        /// <returns>Nothing</returns>
        public static IEnumerator TextDisolveAnim(this Text text)
        {
            StringBuilder stringBuilder = new StringBuilder(text.text);
            List<TextData> textDatas = new List<TextData>();

            for (short i = 0; i < text.text.Length; i++){
                textDatas.Add(new TextData(text.text[i],i));
            }

            while (textDatas.Count > 0){
                var textData = textDatas[RandomReal.Range(0, textDatas.Count)];
                stringBuilder[textData.id] = Space;
                text.text = stringBuilder.ToString();
                textDatas.Remove(textData);
                yield return TextUpdateTime;
            }
            
            text.text = string.Empty;
            yield return null;
        }
        
        private struct TextData
        {
            public char _char;
            public short id;

            public TextData(char _char, short id)
            {
                this._char = _char;
                this.id = id;
            }
        }
    }
}