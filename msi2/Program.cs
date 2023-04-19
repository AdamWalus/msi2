double Fitness(int[] ustawienie)
{
    double fitness = 0;
    // trudność naciśnięcia poszczególnych klawiszy
    double[] trudnosc = { 4, 2, 2, 3, 4, 5, 3, 2, 2, 4, 1.5, 1, 1, 3, 3, 1, 1, 1, 1.5, 4, 4, 3, 2, 5, 3, 2, 3, 4, 4 };
    // częstotliwość użycia liter alfabetu angielskiego   
    double[] czestotliwosc = { 0.082, 0.015, 0.028, 0.043, 0.13, 0.022, 0.02, 0.061, 0.15, 0.019, 0, 0.063, 0.043, 0.028, 0.024, 0.072, 0, 0.077, 0.02, 0.002, 0.008, 0.05, 0.003, 0.075, 0.02, 0.007, 0.04, 0.024, 0.034 };
    for (int i = 0; i < ustawienie.Length; i++)
    {
        fitness += czestotliwosc[i] / trudnosc[ustawienie[i]];
    }
    return fitness;
}






int[] wzorzec = { 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1 };

int N = 100, L = wzorzec.Length;
int[][] rodzice = new int[N][];
for (int i = 0; i < N; i++)
    rodzice[i] = new int[L];
int[] fitness = new int[N];
int[][] dzieci = new int[N][];
for (int i = 0; i < N; i++)
    dzieci[i] = new int[L];

//generujemy N losowych osobników (rodzicow)
Random random = new Random();
for (int i = 0; i < N; i++)
    for (int j = 0; j < L; j++)
        rodzice[i][j] = random.Next(2);



for (int f = 0; f < 100; f++)
{
    for (int i = 0; i < N; i++)
    {
        fitness[i] = 0;
        //F(i) obliczamy fitness osobników
        for (int j = 0; j < L; j++)
        {
            if (rodzice[i][j] == wzorzec[j])
                fitness[i]++;
        }

        if (fitness[i] == L)
        {
            Console.WriteLine("znaleziono po " + f + " epokach");
            goto koniec;
        }

    }

    //krzyzowanie
    for (int i = 0; i < N; i++)
    {
        //selekcja turniejowa
        //losujemy OsA i OsB, lepszy z nich staje sie pierwszym rodzicem
        //X=2,4,8  kandydatow

        int R = 8;
        int[] punktPodzialu = new int[R + 1];
        int[] Rdz = new int[R];

        for (int r = 0; r < R; r++)
        {
            int osA = random.Next(N);
            int osB = random.Next(N);
            Rdz[r] = fitness[osA] > fitness[osB] ? osA : osB;
            punktPodzialu[r] = random.Next(L);
        }


        Array.Sort(punktPodzialu);
        punktPodzialu[0] = 0;
        punktPodzialu[R] = L;

        for (int r = 0; r < R; r++)
        {
            for (int j = punktPodzialu[r]; j < punktPodzialu[r + 1]; j++)
                dzieci[i][j] = rodzice[Rdz[r]][j];
        }

        //  for (int j = 0; j < L; j++)
        //    dzieci[i][j] = random.NextDouble() < 0.5 ? rodzice[Rdz[0]][j] : rodzice[Rdz[1]][j];
    }

    //mutacja

    for (int i = 0; i < N; i++)
        for (int j = 0; j < L; j++)
            rodzice[i][j] = dzieci[i][j];

    //dzieci zastępują rodziców




    Console.WriteLine("Ustawienie klawiatury:");
    for (int i = 0; i < N; i++)
    {
        if (fitness[i] == L)
        {
            Console.WriteLine("Klawisz: " + i + " : " + rodzice[i][0]);
            for (int j = 1; j < L; j++)
                Console.Write(rodzice[i][j] + " ");
            Console.WriteLine();
        }
    }
}
Console.WriteLine("max fitness = " + fitness.Max());

koniec:
;