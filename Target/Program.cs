using System.Text.Json;
#region Primeira Questão
int ind = 13, soma = 0, k = 0;
while (k < ind)
{
    k = (k + 1);
    soma = soma + k;
}
Console.WriteLine(soma);

#endregion


#region Segunda Questão

Console.Write("Informe um número: ");
int numero = int.Parse(Console.ReadLine());

if (CheckFibonacci(numero))
{
    Console.WriteLine($"{numero} pertence à sequência de Fibonacci.");
}
else
{
    Console.WriteLine($"{numero} não pertence à sequência de Fibonacci.");
}

static bool CheckFibonacci(int num)
{
    if (num < 0) return false;

    int a = 0;
    int b = 1;

    if (num == a || num == b) return true;

    while (true)
    {
        int prox = a + b;
        if (prox == num) return true;
        if (prox > num) return false;

        a = b;
        b = prox;
    }
}

#endregion


#region Terceira Questão - Json
try
{
    string currentDirectory = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net8.0", "");

    string json = File.ReadAllText($"{currentDirectory}\\dados.json");

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    List<DayBilling> billing = JsonSerializer.Deserialize<List<DayBilling>>(json, options);

    var validDays = billing.Where(d => d.Valor > 0).ToList();

    if (validDays.Count == 0)
    {
        Console.WriteLine("Não há dados válidos para cálculo.");
        return;
    }

    double minValues = validDays.Min(d => d.Valor);
    double maxValues = validDays.Max(d => d.Valor);

    double media = validDays.Average(d => d.Valor);

    int daysAverage = validDays.Count(d => d.Valor > media);

    Console.WriteLine($"Menor faturamento: R$ {minValues:F2}");
    Console.WriteLine($"Maior faturamento: R$ {maxValues:F2}");
    Console.WriteLine($"Media de faturamento: R$ {media:F2}");
    Console.WriteLine($"Dias com faturamento acima da média: {daysAverage} dias");



}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

#endregion


#region Quarta Questão

var billings = MensalBilling.inicialize();
var total = billings.Sum(b => b.Billing);

MensalBilling.calcParticipate(total, billings);


#endregion

#region Quinta Questão


Console.WriteLine("Qual a entrada que deseja inverter?");
string input = Console.ReadLine();

string reversed = InvertString(input);
Console.WriteLine($"String invertida: {reversed}");

static string InvertString(string input)
{
    if (string.IsNullOrEmpty(input))
        return input;

    char[] characters = input.ToCharArray();
    int start = 0;
    int end = characters.Length - 1;

    while (start < end)
    {
        (characters[start],characters[end]) = (characters[end], characters[start]);
        start++;
        end--;
    }
    return new string(characters);
}


#endregion



#region Classes
public class MensalBilling
{
    public MensalBilling(string state, string billing)
    {
        State = state;
        Billing = Convert.ToDouble(billing);
    }
    public string State { get; set; }
    public double Billing { get; set; }
    public static List<MensalBilling> inicialize()
    {
        List<MensalBilling> mensalBilling = new();
        mensalBilling.Add(new("SP", "67.836,43"));
        mensalBilling.Add(new("RJ", "36.678,66"));
        mensalBilling.Add(new("MG", "29.229,88"));
        mensalBilling.Add(new("ES", "27.165,48"));
        mensalBilling.Add(new("Outros", "19.849,53"));
        return mensalBilling;
    }


    public static void calcParticipate(double total, List<MensalBilling> mensalBillings)
    {
        Console.WriteLine($"Total Mensal: R$ {total}");
        foreach (var item in mensalBillings)
        {
            Console.WriteLine($"Estado - {item.State} \nParticipação - {(item.Billing / total) * 100:F2}%");
        }

    }
}
public class DayBilling
{
    public int Dia { get; set; }
    public double Valor { get; set; }
}

#endregion