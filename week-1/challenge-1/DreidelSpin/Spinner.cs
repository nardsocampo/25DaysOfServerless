using System;

namespace ChallengeApp
{
    public class Spinner
    {
        private readonly Dreidel[] dreidels;
        private readonly Random _rand = new Random();
        public Spinner()
        {
            dreidels = new Dreidel[]
                        {
                            new Dreidel { Code = "נ", Name = "Nun" },
                            new Dreidel { Code = "ג", Name = "Gimmel" },
                            new Dreidel { Code = "ה", Name = "Hay" },
                            new Dreidel { Code = "ש", Name = "Shin" }
                        };
        }

        public Dreidel Spin()
        {
            return dreidels[_rand.Next(1, dreidels.Length) - 1];
        }
    }
}