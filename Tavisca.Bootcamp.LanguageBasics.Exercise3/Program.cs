using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class 
        Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int lenProtein = protein.Length;
            int[] calorie = new int[lenProtein];
            for (int i = 0; i < lenProtein; i++)
            { calorie[i] = protein[i] * 5 + carbs[i] * 5 + fat[i] * 9; }
            int[] result = new int[dietPlans.Length];

            for (int i = 0; i < dietPlans.Length; i++)
            {
                List<int> allindex = new List<int>();
                for (int v = 0; v < lenProtein; v++)
                {
                    allindex.Add(v);
                }
                int high = 0;
                int low = 0;
                char[] dietArray = dietPlans[i].ToCharArray();
                if (dietArray.Length == 0)
                {
                    result[i] = 0;
                    continue;
                }
                for (int j = 0; j < dietArray.Length; j++)
                {
                    switch (dietArray[j])
                    {
                        case 'C':
                            high = highval(carbs, allindex);
                            allindex = Indexofall(carbs, allindex, high);
                            break;
                        case 'c':
                            low = lowVal(carbs, allindex);
                            allindex = Indexofall(carbs, allindex, low);
                            break;
                        case 'P':
                            high = highval(protein, allindex);
                            allindex = Indexofall(protein, allindex, high);
                            break;
                        case 'p':
                            low = lowVal(protein, allindex);
                            allindex = Indexofall(protein, allindex, low);
                            break;
                        case 'F':
                            high = highval(fat, allindex);
                            allindex = Indexofall(fat, allindex, high);
                            break;
                        case 'f':
                            low = lowVal(fat, allindex);
                            allindex = Indexofall(fat, allindex, low);
                            break;
                        case 'T':
                            high = highval(calorie, allindex);
                            allindex = Indexofall(calorie, allindex, high);
                            break;
                        case 't':
                            low = lowVal(calorie, allindex);
                            allindex = Indexofall(calorie, allindex, low);
                            break;
                    }
                    if (allindex.Count == 1)
                    {
                        result[i] = allindex[0];
                        break;
                    }
                }
                result[i] = allindex[0];
            }
            return result;
        }
        public static int highval(int[] content, List<int> allindex)
        {
            int high_res = content[allindex[0]];
            if (allindex.Count != 1)
            {
                foreach (int i in allindex)
                {
                    if (high_res < content[i])
                    {
                        high_res = content[i];
                    }
                }
            }
            return high_res;
        }
        public static int lowVal(int[] content, List<int> allindex)
        {
            int low_result = content[allindex[0]];
            if (allindex.Count != 1)
            {
                foreach (int i in allindex)
                {
                    if (low_result > content[i])
                    {
                        low_result = content[i];
                    }
                }
            }
            return low_result;
        }

        public static List<int> Indexofall(int[] content, List<int> allindex, int val)
        {
            List<int> index = new List<int>();
            foreach (int x in allindex)
            {
                if (content[x] == val)
                {
                    index.Add(x);
                }
            }
            return index;
        }
    }
}
