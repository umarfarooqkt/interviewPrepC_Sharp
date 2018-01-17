using System.IO;
using System;

namespace part1solution
{
    class Part1
    {
        static void Main()
        {
            Boolean check = true;
            Boolean redo = true;
            string[] digits = null;
            while (check)
            {
                check = false;
                // get user input for the filename 
                Console.WriteLine("Enter Filename please:"); // Prompt
                string filename = Console.ReadLine(); // Get string from user
                                                      

                //to add the path to the file name 
                var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
                try
                {
                    // Read each line of the file into a string array. Each element
                    // of the array is one element of the file.
                    digits = System.IO.File.ReadAllLines(path);
                }
                catch (Exception e)
                {
                    // to make sure the user has to re-enetr the correct file
                    Console.WriteLine("The file was not found please try again");
                    check = true;
                }
                finally
                {
                }
            }
            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("The file contains the following numbers ");
            PrintStrArray(digits);

            // The following code gets the user input 
            while (redo)
            {
                redo = false;
                Console.WriteLine();
                Console.WriteLine("Please chose the sort type \n(d for Detection sort, s for string sort, h for Hybrid sort): ");
                char sortType = Console.ReadKey().KeyChar;
                // convert the array to numbers array 
                int[] intDigits = Array.ConvertAll(digits, int.Parse);
                Console.WriteLine();

                if (sortType == 'd' || sortType == 'D')
                {
                   
                    // sorting the int array ascendingly
                    int[] intDigitsSorted = AscendingSort(intDigits);
                    Console.WriteLine("You chose Detection sort and your result is");
                    PrintIntArray(intDigitsSorted);
                }
                else if (sortType == 's' || sortType == 'S')
                {
                    // get the first digit of the integer 
                    int[] firstDigit = GetFirstDigit(digits);
                    // now I will sort the first digit array acesndingly and use it as an index array to sort the original int array
                    Console.WriteLine("You chose String sort and your result is");
                    // PrintIntArray( StringSort(firstDigit, intDigits));
                    Array.Sort(digits);
                    PrintStrArray(digits);
                                   
                }
                else if (sortType == 'h' || sortType == 'H')
                {
                    //hybrid sort the integers 
                    PrintIntArray(HybridSort(intDigits));
                }
                else
                {
                    Console.WriteLine("That was not a correct sort selection please try again");
                    redo = true;
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        /// <summary>
        ///  All the externel methods used in the program
        /// </summary>
        /// 

        // get the array of the first digit 
        static int[] GetFirstDigit( String[] array)
        {
            String[] darray = new String[array.Length];
            for (int i =0; i < darray.Length; i++)
            {
                darray[i] = array[i].Substring(0,1);
            }
            int[] intdarray = Array.ConvertAll(darray, int.Parse);

            return intdarray;
        }

        // print string array 
        static void PrintStrArray(String[] array)
        {
            Console.WriteLine();
            foreach (String item in array)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
        }

        // print int array 
        static void PrintIntArray(int[] array)
        {
            Console.WriteLine();
            foreach (var item in array)
            {
                Console.Write(item.ToString() + ", ");
            }
            Console.WriteLine();

        }

        // detection sorting method used counting sort here probably going to be O(n)
        static int[] AscendingSort(int[] numbers)
        {
            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                    max = numbers[i];
            }

            int[] sortedNumbers = new int[max + 1];

            for (int i = 0; i < numbers.Length; i++)
            {
                sortedNumbers[numbers[i]]++;
            }

            int insertPosition = 0;

            for (int i = 0; i <= max; i++)
            {
                for (int j = 0; j < sortedNumbers[i]; j++)
                {
                    numbers[insertPosition] = i;
                    insertPosition++;
                }
            }
            return numbers;
        }

        // detection sorting method used counting sort here probably going to be O(n)
        static void DescendingSort(int[] numbers)
        {
            int min = int.MaxValue, max = int.MinValue;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }

                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            int[] counts = new int[max - min + 1];

            for (int i = 0; i < numbers.Length; i++)
            {
                counts[numbers[i] - min]++;
            }

            int k = 0;

            for (int j = max; j >= min; j--)
            {
                for (int i = 0; i < counts[j - min]; i++)
                {
                    numbers[k++] = j;
                }
            }
        }

         // Hybrid sort for the array 
        static int[] HybridSort(int[] num)
        {
            int[] sortedArray = new int[num.Length];
            int[] even = new int[num.Length];
            int[] odd = new int[num.Length];
            int odd_i = 0;
            int even_i = 0;

            // Seperate the odds and evens in two different arrays 
            for (int i= 0; i < num.Length; i++ )
            {
               if(num[i]%2 == 0)
                {
                    even[even_i] = num[i];
                    even_i++;
                }
                else
                {
                    odd[odd_i] = num[i];
                    odd_i++;
                }
            }
            // to asort both the arrays
            odd = AscendingSort(odd);
            DescendingSort(even);
            
            for (int e = 0; e < even_i; e++)
            {
                sortedArray[e] = even[e];
            }
            for (int o = even_i; o < sortedArray.Length; o++)
            {
                sortedArray[o] = odd[o];
            }  

            return sortedArray;
        }
    }
}