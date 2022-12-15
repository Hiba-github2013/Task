using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
 
 
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

 
using Microsoft.AspNetCore.Mvc.RazorPages;
 
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
 
using System.Xml.Linq;

using Kronos.TestTask.core;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Forms;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq.Expressions;

namespace WebAPI1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {          
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PdfResult> Get()
        {
            string fileName = "RequestBody.txt";
            string jsonString = System.IO.File.ReadAllText(fileName);

            Game weatherForecast = JsonConvert.DeserializeObject<Game>(jsonString)!;
          
            List<PdfResult> DataList = new List<PdfResult> { };

            List<Result> R = weatherForecast.results;

            for (int i = 0; i < R.Count; i++)
            {
                Result R1 = R[i];
                PdfResult D1 = new PdfResult();
                D1.gamedate = R1.gamedate.ToShortDateString();
                     Hmteam hmteam1 = new Hmteam();
            hmteam1.id = R1.hmteam.id; hmteam1.name = R1.hmteam.name;
            hmteam1.delegation= R1.hmteam.delegation;
                //Calcule Start Time
                int H = R1.gamedate.Hour;
                int Min = R1.gamedate.Minute;
                string HH = H.ToString();
                string Minn=Min.ToString();
                if (H == 0) { HH = "00"; }
                if (H< 10) { HH = "0"+ H; }
                if (Min == 0) { Minn = "00"; }
                if (Min < 10) { Minn = "0" + Min; }
                D1.Time = HH + ":" + Minn;
                //Calcule End Time
                  H = R1.gamedateend.Hour;
                  Min = R1.gamedateend.Minute;
                  HH = H.ToString();
                  Minn = Min.ToString();
                if (H == 0) { HH = "00"; }
                if (H < 10) { HH = "0" + H; }
                if (Min == 0) { Minn = "00"; }
                if (Min < 10) { Minn = "0" + Min; }
                D1.gamedateend = HH + ":" + Minn;
                D1.location = R1.location;
            Awteam awteam1 = new Awteam();
                awteam1.id = R1.awteam.id; awteam1.name = R1.awteam.name;
                awteam1.delegation = R1.awteam.delegation;
                D1.hmteam = hmteam1;
                D1.awteam = awteam1;
                DataList.Add(D1);

            }

                    
 
            string dataDir = "";

            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();
          Aspose.Pdf.Page p=   pdfDocument.Pages.Add(); p.SetPageSize(1188, 1188);

            // Initializes a new instance of the Table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                  };
             

            //set the column widths of the table

            table.ColumnWidths = "100 100 100 100 100 100 150 100 100";
           



            //Set the font name to "Co

            Aspose.Pdf.Row row1 = table.Rows.Add();
            row1.BackgroundColor = Color.Orange;
            row1.MinRowHeight = 30;

            // Add table cells
            row1.Cells.Add("Name");
            row1.Cells.Add("Pool");
            row1.Cells.Add("Home Team");
            row1.Cells.Add("     ");
            row1.Cells.Add("Away Team");
            row1.Cells.Add("Loc.");
            row1.Cells.Add("Date");
            row1.Cells.Add("Time");
            row1.Cells.Add("End Time");
            for (int i = 0; i < DataList.Count; i++)
            {
                PdfResult R1 = DataList[i];

                Aspose.Pdf.Row row2 = table.Rows.Add();
                row2.MinRowHeight = 30;

                if (i % 2 == 0) {  row2.BackgroundColor = Color.White;
               }
                if (i % 2 == 1)
                {
                    row2.BackgroundColor = Color.WhiteSmoke;
                }

                if (R1.gamename == null) {

                    row2.Cells.Add("");
                 
              }
                else 
                {
                    row2.Cells.Add(R1.gamename);
                   
                }
                row2.Cells.Add("");
                row2.Cells.Add(R1.hmteam.name);
                
                row2.Cells.Add("   VS   ");
                 row2.Cells.Add(R1.awteam.name);
                row2.Cells.Add(R1.location.ToString());
                row2.Cells.Add(R1.gamedate);
                row2.Cells.Add(R1.Time);
                row2.Cells.Add(R1.gamedateend);


            }

            //  //// Add text to new page
            //   page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello World!"));
            //  //// Save updated PDF
            //var outputFileName = System.IO.Path.Combine(dataDir, "HelloWorld_out.pdf");
            //pdfDocument.Save(outputFileName);

            // Add table object to first page of input document
            pdfDocument.Pages[1].Paragraphs.Add(table);

            // Save updated document containing table object
            pdfDocument.Save(Path.Combine(dataDir, "document_with_table_out.pdf"));

            return DataList;
            
           

        }
     


    }
    
    }
