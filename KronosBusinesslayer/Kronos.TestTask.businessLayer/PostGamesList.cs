using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Kronos.TestTask.Core;

namespace Kronos.TestTask.businessLayer
{

// WebApi Interface
    public interface IGames
    {
        List<PdfResult> PostGamesList(List<Result> Results);
    }
  
    public class PostGamesForPDF : IGames
    {
        public List<PdfResult> PostGamesList(List<Result> Results)
        {   
            List<PdfResult> DataList = new List<PdfResult> { };
            for (int i = 0; i < Results.Count; i++)
            {

                PdfResult D1 = new PdfResult();
                D1.Gamedate = Results[i].Gamedate.ToShortDateString();
                Hmteam hmteam1 = new Hmteam();
                hmteam1.ID = Results[i].Hmteam.ID; hmteam1.Name = Results[i].Hmteam.Name;
                hmteam1.Delegation = Results[i].Hmteam.Delegation;
                //Calcule Start Time
                int H = Results[i].Gamedate.Hour;
                int Min = Results[i].Gamedate.Minute;
                string HH = H.ToString();
                string Minn = Min.ToString();
                if (H == 0) { HH = "00"; }
                if (H < 10) { HH = "0" + H; }
                if (Min == 0) { Minn = "00"; }
                if (Min < 10) { Minn = "0" + Min; }
                D1.Time = HH + ":" + Minn;
                //Calcule End Time
                H = Results[i].Gamedateend.Hour;
                Min = Results[i].Gamedateend.Minute;
                HH = H.ToString();
                Minn = Min.ToString();
                if (H == 0) { HH = "00"; }
                if (H < 10) { HH = "0" + H; }
                if (Min == 0) { Minn = "00"; }
                if (Min < 10) { Minn = "0" + Min; }
                D1.Gamedateend = HH + ":" + Minn;
                D1.Location = Results[i].Location;
                Awteam awteam1 = new Awteam();
                awteam1.ID = Results[i].Awteam.ID; awteam1.Name = Results[i].Awteam.Name;
                awteam1.Delegation = Results[i].Awteam.Delegation;
                D1.Hmteam = hmteam1;
                D1.Awteam = awteam1;
                DataList.Add(D1);

            }

            return DataList;


        }
    }




    // Azure Function Interface
   public interface IGamesAzureFunction
    {
        List<DataToDisplay> PostGamesList(List<Result> Results);
    }   public class PostGames : IGamesAzureFunction
    {
        public List<DataToDisplay> PostGamesList(List<Result> Results)
        {
            List<DataToDisplay> DataList = new List<DataToDisplay> { };
            for (int i = 0; i < Results.Count; i++)
            {
                 DataToDisplay D1 = new DataToDisplay();
                D1.Gamedateend = Results[i].Gamedateend;
                D1.Gamedate = Results[i].Gamedate;
                D1.ID = Results[i].ID;
               
                DataList.Add(D1);

            }

            return DataList;


        }
    }
}

