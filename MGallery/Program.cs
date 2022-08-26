// */1 * * * * /home/pi/.dotnet/dotnet /home/pi/mgall/genshin/MGallery.dll

using System.Reflection;

string gallid = "onshinproject";

string urlMain = $"https://gall.dcinside.com/mgallery/board/lists/?id={gallid}";
string urlRecommend = $"https://gall.dcinside.com/mgallery/board/lists/?id={gallid}&exception_mode=recommend";

HttpClient client = new HttpClient();

//Console.WriteLine(htmlMain);

var assembly = System.Reflection.Assembly.GetEntryAssembly();

var now = DateTime.Now;
string time = now.ToString("HHmmss");
string date = now.ToString("yyMMdd");

if (assembly != null)
{
    Console.WriteLine($"main : {urlMain}");
    Console.WriteLine($"recommend : {urlRecommend}");

    await R100(urlMain, urlRecommend, client, assembly, time, date);
    //await M4R1(urlMain, urlRecommend, client, assembly, time, date);

}

static async Task M4R1(string urlMain, string urlRecommend, HttpClient client, Assembly? assembly, string time, string date)
{
    string? exePath = assembly.Location;
    exePath = Path.Combine(Path.GetDirectoryName(exePath), date);
    Console.WriteLine("exePath : " + exePath);
    Console.WriteLine();
    if (!Directory.Exists(exePath)) Directory.CreateDirectory(exePath);
    Directory.SetCurrentDirectory(exePath);

    for (int page = 1; page <= 4; page++)
    {
        var url = $"{urlMain}&page={page}";
        Console.WriteLine(url);
        File.WriteAllText($"m_{time}_{page}.htm", await client.GetStringAsync(url));
    }

    Console.WriteLine(urlRecommend);
    File.WriteAllText($"r_{time}.htm", await client.GetStringAsync(urlRecommend));
}


static async Task R100(string urlMain, string urlRecommend, HttpClient client, Assembly? assembly, string time, string date)
{
    string? exePath = assembly.Location;
    exePath = Path.Combine(Path.GetDirectoryName(exePath), date + "r100");
    Console.WriteLine("exePath : " + exePath);
    Console.WriteLine();
    if (!Directory.Exists(exePath)) Directory.CreateDirectory(exePath);
    Directory.SetCurrentDirectory(exePath);
    for (int page = 1; page <= 100; page++)
    {
        var url = $"{urlRecommend}&page={page}";
        Console.WriteLine(url);
        File.WriteAllText($"r_{time}_{page}.htm", await client.GetStringAsync(url));
    }
}
