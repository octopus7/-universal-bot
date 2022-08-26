// */1 * * * * /home/pi/.dotnet/dotnet /home/pi/mgall/genshin/MGallery.dll

string gallid = "onshinproject";
string urlMain = $"https://gall.dcinside.com/mgallery/board/lists?id={gallid}";
string urlRecommend = $"https://gall.dcinside.com/mgallery/board/lists?id={gallid}&exception_mode=recommend";

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

    string? exePath = assembly.Location;
    exePath = Path.Combine(Path.GetDirectoryName(exePath), date);
    Console.WriteLine("exePath : " + exePath);
    Console.WriteLine();
    if (!Directory.Exists(exePath)) Directory.CreateDirectory(exePath);
    Directory.SetCurrentDirectory(exePath);

    string? htmlMain = await client.GetStringAsync(urlMain);
    string? htmlRecommend = await client.GetStringAsync(urlRecommend);

    File.WriteAllText("m_" + time + ".htm", htmlMain);
    File.WriteAllText("r_" + time  + ".htm", htmlRecommend);
}

