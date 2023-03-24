using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Messaging;

namespace ConsoleApp2
{
    internal class Program
    {
        public class Question
        {
            public int QuestionNumber;
            public string Description;
            public string A1;
            public string A2;
            public string A3;
            public string A4;
            public void Go(string d,string a1, string a2, string a3, string a4)
            {
                Description = d;
                A1 = a1;
                A2 = a2;
                A3 = a3;
                A4 = a4;
            }
        }
        

        static void Main(string[] args)
        {
            int Check(int a)
            {
                if (a > 0 && a < 5)
                {
                    return a;
                }
                else
                {
                    Console.WriteLine("Число должно быть от 1-4, введите другое число");
                    int ans = int.Parse(Console.ReadLine());
                    return Check(ans);
                }
            }
            int i = 0;
            string s;
            string name;
            int score = 0;
            try
            {
                using (StreamReader sr = File.OpenText("bank.txt") )
                {
                    var lineCount = File.ReadLines("bank.txt").Count();
                    Question[] questions = new Question[lineCount];
                    for (int j = 0; j < lineCount; j++)
                    {
                        questions[j] = new Question();
                    }
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] s1 = s.Replace(@"||","|").Split('|');
                        questions[i].Go( s1[0], s1[1], s1[2], s1[3], s1[4]);
                        i++;
                    }
                    Console.WriteLine("Введите имя");
                    name = Console.ReadLine();
                    Console.WriteLine("Во время игры за каждый правильный ответ начисляется по одному очку");
                    for (int j = 0; j < lineCount; j++)
                    {
                        Console.WriteLine(questions[j].Description);
                        Console.WriteLine("1: " + Regex.Replace(questions[j].A1, @"(=>False|=>True)", ""));
                        Console.WriteLine("2: " + Regex.Replace(questions[j].A2, @"(=>False|=>True)", ""));
                        Console.WriteLine("3: " + Regex.Replace(questions[j].A3, @"(=>False|=>True)", ""));
                        Console.WriteLine("4: " + Regex.Replace(questions[j].A4, @"(=>False|=>True)", ""));
                        Console.WriteLine("Введите номер вашего ответа");
                        int ans = int.Parse(Console.ReadLine());
                        Check(ans);
                        switch (ans)
                        {
                            case 1:
                                if (Regex.IsMatch(questions[j].A1, @"=>True")) 
                                {
                                    Console.WriteLine("Ответ правильный");
                                    score++;
                                }
                                else
                                    Console.WriteLine("Ответ неправильный");
                                break;
                            case 2:
                                if (Regex.IsMatch(questions[j].A2, @"=>True")) 
                                {
                                    Console.WriteLine("Ответ правильный");
                                    score++;
                                }
                                else
                                    Console.WriteLine("Ответ неправильный");
                                break;
                            case 3:
                                if (Regex.IsMatch(questions[j].A3, @"=>True"))
                                {
                                    Console.WriteLine("Ответ правильный");
                                    score++;
                                }
                                else
                                    Console.WriteLine("Ответ неправильный");
                                break;
                            case 4:
                                if (Regex.IsMatch(questions[j].A4, @"=>True"))
                                {
                                    Console.WriteLine("Ответ правильный");
                                    score++;
                                }
                                else
                                    Console.WriteLine("Ответ неправильный");
                                break;

                        }
                        

                    }
                    Console.WriteLine(score);
                    try
                    {
                        using (var fs = new StreamWriter(new FileStream("Leader.txt", FileMode.Append)))
                        
                        {
                            
                            fs.WriteLine(name + " " + score);
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine($"Файл не найден: {e.Message}");
                    }
                    try
                    {
                        using (StreamReader fr = File.OpenText("Leader.txt"))
                        {
                            string line1;
                            while ((line1 = fr.ReadLine()) != null)
                            {
                                Console.WriteLine(s);
                            }
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine($"Файл не найден: {e.Message}");
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Файл не найден: {e.Message}");
            }
            
            
        }
    }
}
