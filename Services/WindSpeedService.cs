using WindService.Interfaces;

namespace WindService
{
    public class WindSpeedService : IWindSpeedService
    {
        private struct windS
        {
            public double speed;
            public string description;

            public windS(double speed, string description)
            {
                this.speed = speed;
                this.description = description;
            }
        }

        private readonly windS[] wind = new windS[] {
            new windS(0, "штиль"),
            new windS(0.5, "тихий"),
            new windS(1.6, "легкий"),
            new windS(3.4, "слабкий"),
            new windS(5.5, "помірний"),
            new windS(8.0, "свіжий"),
            new windS(10.8, "сильний"),
            new windS(13.9, "міцний"),
            new windS(17.2, "дуже міцний"),
            new windS(20.8, "шторм"),
            new windS(24.5, "сильний шторм"),
            new windS(28.5, "жорстокий шторм"),
            new windS(32.7, "ураган") };


        public string SpeedDescription(double windSpeed)
        {
            try
            {
                for (int i = 0; i < wind.Length; i++)
                {
                    if (windSpeed < wind[i].speed)
                        return wind[i - 1].description;
                }
                return wind[wind.Length - 1].description;
            }
            catch
            {
                return wind[0].description;
            }

        }
    }
}
