using Unity.Burst.CompilerServices;
using System.Collections.Generic;
using QuestionSystem;
using UnityEngine;

namespace QuestionSystem
{
    [System.Serializable]
    public abstract class Hint
    {
        public string dataBankName;
        public int questionNumber;
        public abstract void TypeOfHint();
    }

    public class TextHint : Hint
    {
        public string text;
        public override void TypeOfHint()
        {
            // texto
        }
    }
    public class ImageHint : Hint
    {
        public string imagePath;
        public override void TypeOfHint()
        {
            // caminho da imagem
        }
    }
    public class LinkHint : Hint
    {
        public string link;
        public override void TypeOfHint()
        {
            // link
        }
    }
}


