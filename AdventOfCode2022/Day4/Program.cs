namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solve().Item1);
            Console.WriteLine(Solve().Item2);
        }

        static (int, int) Solve()
        {
            string[] input = File.ReadAllLines("input.txt");
            int sum = 0;
            int biggersum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string[] sections = input[i].Split(",");
                Section sec1 = new Section(sections[0]);
                Section sec2 = new Section(sections[1]);

                if (sec1.SectionStart >= sec2.SectionStart && sec1.SectionEnd <= sec2.SectionEnd || sec2.SectionStart >= sec1.SectionStart && sec2.SectionEnd <= sec1.SectionEnd)
                    sum++;

                Section smallerSection = sec1.SectionSize > sec2.SectionSize ? sec2 : sec1;
                Section biggerSection = sec1.SectionSize > sec2.SectionSize ? sec1 : sec2;

                // default values
                if (sec1.SectionSize == sec2.SectionSize)
                {
                    smallerSection = sec1;
                    biggerSection = sec2;
                }

                for (int pos = biggerSection.SectionStart; pos <= biggerSection.SectionEnd; pos++)
                {
                    if (pos >= smallerSection.SectionStart && pos <= smallerSection.SectionEnd)
                    {
                        biggersum++;
                        break;
                    }
                }
            }

            return (sum, biggersum);
        }
    }

    public class Section
    {
        public int SectionStart { get; set; }
        public int SectionEnd { get; set; }
        public int SectionSize { get; set; }

        public Section(string section)
        {
            SectionStart = int.Parse(section.Split("-")[0]);
            SectionEnd = int.Parse(section.Split("-")[1]);
            SectionSize = SectionEnd - SectionStart;
        }

    }
}