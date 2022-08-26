// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using System;
using System.IO;
using System.Text;
using System.Web;

List<string> xpaths = new List<string>();

Console.WriteLine("");


// 로 노드찾아서 적당한 뎁스에서 추출
// FindXPath("m_235902.htm", "화신춤");
// SaveXPath();


//var path = "m_235902.htm";

GetFiles();

void GetFiles()
{
    // 조회수 오르는 속도 추천 오르는 속도

    var paths = Directory.GetFiles(".", "m_*");

    foreach (var path in paths)
    {
        Console.WriteLine("----------" + path);
        parseList(path);
    }    
}

void parseList(string path)
{
    string xpath = "/html[1]/body[1]/div[2]/div[3]/main[1]/section[1]/article[2]/div[2]/table[1]/tbody[1]/tr[*]";

    string html = File.ReadAllText(path);
    HtmlDocument doc = new HtmlDocument();
    doc.LoadHtml(html);
    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
    if (nodes == null) return;

    foreach (var node in nodes)
    {
        HtmlDocument docR = new HtmlDocument();
        docR.LoadHtml(node.InnerHtml);
        //Console.WriteLine(node.InnerHtml);
        //Console.WriteLine("-----");
        var cate = docR.DocumentNode.SelectSingleNode("/td[2]").InnerText;

        if (cate.Equals("설문") || cate.Equals("이슈") || cate.Equals("공지"))
        {
            continue;
        }

        var no = docR.DocumentNode.SelectSingleNode("/td[1]").InnerText;
        var title = docR.DocumentNode.SelectSingleNode("/td[3]").InnerText.Trim().Replace("\t", "").Replace("\r", "").Replace("\n", "");
        var nick = docR.DocumentNode.SelectSingleNode("/td[4]").InnerText;
        var timestr = docR.DocumentNode.SelectSingleNode("/td[5]").Attributes["title"].Value;
        var views = docR.DocumentNode.SelectSingleNode("/td[6]").InnerText;
        var vote = docR.DocumentNode.SelectSingleNode("/td[7]").InnerText;
        Console.WriteLine($"{no} {cate} {views} {vote} {timestr} {title}");
    }
}


void FindXPath(string path1, string word1)
{
    HtmlDocument doc1 = new HtmlDocument();
    doc1.LoadHtml(File.ReadAllText(path1));
    var nodes1 = doc1.DocumentNode.SelectNodes("//*");
    foreach (var node in nodes1)
    {
        //if (node.InnerText.IndexOf(word1) == 0)
        if (node.InnerText.Contains(word1))
        {
            //Console.WriteLine();
            xpaths.Add(word1 + ":" + node.XPath);
        }
    }
}

void SaveXPath()
{
    StreamWriter sw = new StreamWriter("xpath.txt");
    foreach (var xpath in xpaths)
    {
        sw.WriteLine(xpath);
        Console.WriteLine(xpath);
    }
    sw.Close();
}