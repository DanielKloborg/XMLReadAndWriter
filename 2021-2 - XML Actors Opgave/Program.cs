using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Actors {

  public class Program {

    private static void Main() {
      // 1. Getting the XML from Actors.Xml
      List<Actor> actors = ReadXml("actors.xml");

      // 2. Writing it in console 
      foreach (Actor actor in actors)
        Console.WriteLine(actor);

      // 3. Writing it out in Actors2.xml
      WriteXml(actors, "../../../actors2.xml");
    }

    private static List<Actor> ReadXml(string path) {
      List<Actor> result = new List<Actor>(); //List of objects Actors

      //Settings for the reader
      XmlReaderSettings settings = new XmlReaderSettings {
        IgnoreComments = true,
        IgnoreWhitespace = true
      };
        
      using (XmlReader reader = XmlReader.Create(path, settings)) {
        reader.MoveToContent(); //Moves to first content node.

        //Finds the start element is actors
        reader.ReadStartElement("actors");
        {
          while (reader.IsStartElement("actor")) //For each actor in the file, it will make a new objet in List
            result.Add(ReadActor(reader)); //Using function ReadActor to find all elements
        }
        reader.ReadEndElement(); // </actors>
      }

      return result;
    }

    private static Actor ReadActor(XmlReader reader) {
      Actor result = new Actor();
      
      //Makes an object of actor, and gets attributes and elements from XML 

      result.Nationality = reader.GetAttribute("nationality");

      reader.ReadStartElement("actor"); 
      {
        while (reader.IsStartElement())
          switch (reader.LocalName) {
            case "name":
              result.Name = reader.ReadElementContentAsString();
              break;

            case "role":
              result.Role = reader.ReadElementContentAsString();
              break;

            case "year":
              result.Year = reader.ReadElementContentAsInt();
              break;

            default:
              Console.WriteLine("unknown actor property: " + reader.LocalName);
              break;
          }
      }
      reader.ReadEndElement(); // </actor>

      return result;
    }

    private static void WriteXml(List<Actor> actors, string path) {
      XmlWriterSettings settings = new XmlWriterSettings {
        Indent = true,
        IndentChars = "  ",
        Encoding = Encoding.UTF8
      };

      using (XmlWriter writer = XmlWriter.Create(path, settings)) {
        writer.WriteStartDocument();
        {
          writer.WriteStartElement("actors");
          {
            foreach (Actor actor in actors)
              WriteActor(writer, actor);
          }
          writer.WriteEndElement(); // </actors>
        }
        writer.WriteEndDocument();
      }
    }

    private static void WriteActor(XmlWriter writer, Actor actor) {
      writer.WriteStartElement("actor");
      {
        writer.WriteAttributeString("nationality", actor.Nationality);

        writer.WriteElementString("name", actor.Name);
        writer.WriteElementString("role", actor.Role);
        writer.WriteElementString("year", actor.Year.ToString());
      }
      writer.WriteEndElement(); // </actor>
    }
  }
}