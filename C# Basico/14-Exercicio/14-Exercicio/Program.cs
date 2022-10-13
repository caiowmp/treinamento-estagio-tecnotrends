/* QUESTÃO 01

Console.WriteLine("Insira o peso");
double peso = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Insira a altura");
double altura = Convert.ToDouble(Console.ReadLine());

double imc = peso / (altura * altura);
Console.WriteLine("IMC: {0}", imc);
*/

/* QUESTÃO 02 
Console.WriteLine("Insira o comprimento");
double comprimento = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Insira a largura");
double largura = Convert.ToDouble(Console.ReadLine());

double area = comprimento * largura;
double perimetro = 2 * comprimento + 2 * largura;
Console.WriteLine("Perimetro: {0}\nArea: {1}", perimetro, area);
*/

/* QUESTÃO 03 
Console.WriteLine("Insira a quantidade de elementos que você deseja inserir");
int n = Convert.ToInt32(Console.ReadLine());
int[] valores = new int[n];
for (int i = 0; i < n; i++)
{
    Console.WriteLine("Insira um valor");
    valores[i] = Convert.ToInt32(Console.ReadLine());
}
Array.Sort(valores);
foreach (int valor in valores) 
{
    Console.Write("{0} ", valor);
}

Console.ReadLine();
*/