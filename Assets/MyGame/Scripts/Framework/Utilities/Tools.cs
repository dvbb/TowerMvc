using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public static class Tools
{
    public static void ParseXml(string fileName, ref LevelInfo level)
    {
        level = new LevelInfo();
        FileInfo file = new FileInfo(fileName);
        StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8);

        XmlDocument doc = new XmlDocument();
        doc.Load(sr);

        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.CardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
        level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);

        XmlNodeList nodes;
        nodes = doc.SelectNodes("/Level/Holder/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Point point = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));
            level.Holder.Add(point);
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Point point = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));
            level.Path.Add(point);
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Round round = new Round(int.Parse(node.Attributes["Monster"].Value), int.Parse(node.Attributes["Count"].Value));
            level.Rounds.Add(round);
        }

        sr.Close();
        sr.Dispose();
    }

    public static void ParseXml(XmlDocument doc, ref LevelInfo level)
    {
        level = new LevelInfo();
        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.CardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
        level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);

        XmlNodeList nodes;
        nodes = doc.SelectNodes("/Level/Holder/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Point point = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));
            level.Holder.Add(point);
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Point point = new Point(int.Parse(node.Attributes["X"].Value), int.Parse(node.Attributes["Y"].Value));
            level.Path.Add(point);
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Round round = new Round(int.Parse(node.Attributes["Monster"].Value), int.Parse(node.Attributes["Count"].Value));
            level.Rounds.Add(round);
        }
    }


    public static void SaveXml(string fileName, LevelInfo level)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\"  encoding=\"utf-8\"?>");
        sb.AppendLine("<Level>");

        // Basic node
        sb.AppendLine(string.Format("<Name>{0}</Name>", level.Name));
        sb.AppendLine(string.Format("<CardImage>{0}</CardImage>", level.CardImage));
        sb.AppendLine(string.Format("<Background>{0}</Background>", level.Background));
        sb.AppendLine(string.Format("<Road>{0}</Road>", level.Road));
        sb.AppendLine(string.Format("<InitScore>{0}</InitScore>", level.InitScore));

        // Holder
        sb.AppendLine("<Holder>");
        foreach (var item in level.Holder)
        {
            sb.AppendLine($"<Point X=\"{item.X}\" Y=\"{item.Y}\"/>");
        }
        sb.AppendLine("</Holder>");

        // Path
        sb.AppendLine("<Path>");
        foreach (var item in level.Path)
        {
            sb.AppendLine($"<Point X=\"{item.X}\" Y=\"{item.Y}\"/>");
        }
        sb.AppendLine("</Path>");

        // Rounds 
        sb.AppendLine("<Rounds>");
        foreach (var item in level.Rounds)
        {
            sb.AppendLine($"<Round Monster=\"{item.Monster}\" Count=\"{item.Count}\"/>");
        }
        sb.AppendLine("</Rounds>");

        sb.AppendLine("</Level>");

        string content = sb.ToString();

        Debug.Log(content);

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.ConformanceLevel = ConformanceLevel.Auto;
        settings.Encoding = Encoding.UTF8;
        settings.IndentChars = "\t";
        settings.OmitXmlDeclaration = false;

        XmlWriter writer = XmlWriter.Create(fileName, settings);

        XmlDocument document = new XmlDocument();
        document.LoadXml(content);
        document.WriteTo(writer);

        writer.Close();
        writer.Dispose();
    }

    //public static IEnumerator LoadImageFromUrl(string url, SpriteRenderer render)
    //{
    //    Debug.Log(url);

    //    WWW www = new WWW(url);
    //    while (!www.isDone)
    //        yield return www;

    //    Texture2D texture = www.texture;
    //    Sprite sp = Sprite.Create(
    //        texture,
    //        new Rect(0, 0, texture.width, texture.height),
    //       new Vector2(.5f, .5f));
    //    render.sprite = sp;
    //}

    public static IEnumerator LoadImage(string url, SpriteRenderer render)
    {
        Texture2D texture = Resources.Load(url) as Texture2D;
        Sprite sp = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(.5f, .5f));
        render.sprite = sp;
        //render.sprite = sprite;
        yield return .1f;
    }

    public static IEnumerator LoadImage(string url, SpriteRenderer render, float pixelsPerUnit)
    {
        Texture2D texture = Resources.Load(url) as Texture2D;
        Sprite sp = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(.5f, .5f),
            pixelsPerUnit);
        render.sprite = sp;
        //render.sprite = sprite;
        yield return .1f;
    }

    public static List<FileInfo> GetLevelFiles()
    {
        string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");
        List<FileInfo> list = new List<FileInfo>();
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            list.Add(fileInfo);
        }
        return list;
    }

    public static FileInfo GetLevelFile(int index)
    {
        string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");
        List<FileInfo> list = new List<FileInfo>();
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            list.Add(fileInfo);
        }
        return list.FirstOrDefault(item => item.Name == $"Level{index}.xml");
    }
}
