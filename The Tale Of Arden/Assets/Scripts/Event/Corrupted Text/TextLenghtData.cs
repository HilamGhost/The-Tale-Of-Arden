using UnityEngine;

namespace Arden.Event
{
    public static class TextLenghtData
    {
        public static Vector2 OneLenghtWord = new Vector2(0.5066f,0.4552f);
        public static Vector2 TwoLenghtWord = new Vector2(0.6737f,0.4552f);
        public static Vector2 ThreeLenghtWord = new Vector2(0.8638f,0.4552f);
        public static Vector2 FourLenghtWord = new Vector2(1.0441f,0.4552f);
        public static Vector2 FiveLenghtWord = new Vector2(1.2768f,0.4552f);
        public static Vector2 SixLenghtWord = new Vector2(1.3817f,0.4552f);
        public static Vector2 SevenLenghtWord = new Vector2(1.5652f,0.4552f);
        public static Vector2 EightLenghtWord = new Vector2(1.8602f,0.4552f); 
        public static Vector2 NineLenghtWord = new Vector2(2.0536f,0.4619f);

        public static Vector2 WantedWord(int lenght)
        {
            Vector2 wantedVector = lenght switch
            {
                1 => OneLenghtWord,
                2 => TwoLenghtWord,
                3 => ThreeLenghtWord,
                4 => FourLenghtWord,
                5 => FiveLenghtWord,
                6 => SixLenghtWord,
                7 => SevenLenghtWord,
                8 => EightLenghtWord,
                9 => NineLenghtWord,
                _ => Vector2.zero
                
            };
            return wantedVector;
        }
        
    }
}
