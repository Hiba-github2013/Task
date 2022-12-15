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
                D1.gamedateend = "0";// (R1.gamedateend-R1.gamedate).ToString;
                D1.gamedate = R1.gamedate;
               // D1.id = R1.id;
              Hmteam hmteam1 = new Hmteam();
            hmteam1.id = R1.hmteam.id; hmteam1.name = R1.hmteam.name;
            hmteam1.delegation= R1.hmteam.delegation;
                D1.Time = "9:00";
                D1.location = R1.location;
            Awteam awteam1 = new Awteam();
                awteam1.id = R1.awteam.id; awteam1.name = R1.awteam.name;
                awteam1.delegation = R1.awteam.delegation;
                D1.hmteam = hmteam1;
                D1.awteam = awteam1;
                DataList.Add(D1);

            }

                    

            Aspose.Pdf.License license = new Aspose.Pdf.License();

            string dataDir = "";

            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();
            pdfDocument.Pages.Add();

            // Initializes a new instance of the Table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Set the table border color as LightGray
                Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Color.Black),
                // Set the border for table cells
                DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Color.Black)
            };
           
            Aspose.Pdf.Row row1 = table.Rows.Add();
            row1.BackgroundColor = Color.Orange;
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
                 Aspose.Pdf.Cell c1 = row2.Cells[3];
                c1.Border = null;
               row2.Cells.Add(R1.awteam.name);
                row2.Cells.Add(R1.location.ToString());
                row2.Cells.Add(R1.gamedate.Date.ToString());
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
    //public class DocumentGenerationService : IDocumentGenerationService
    //{
    //    private string _licence;
    //    public DocumentGenerationService(string licence)
    //    {
    //        _licence = licence;
    //    }
    //    public async Task<byte[]> GenerateDocument(byte[] bytes, ContentType documentType, GetDocumentModel document)
    //    {
    //        var inputMemoryStream = new MemoryStream(bytes);
    //        Aspose.Pdf.License license = new Aspose.Pdf.License();

    //        byte[] byteArray = Encoding.UTF8.GetBytes(_licence);
    //        using (var stream = new MemoryStream(byteArray))
    //        {
    //            license.SetLicense(stream);
    //        }
    //        var PDF = new Aspose.Pdf.Document(inputMemoryStream);
    //         var dataset = Utilities.FunctionUtilities.ConvertJsonToDataset(JsonConvert.SerializeObject(document.PlaceholderValues));
    //        PDF.MailMerge.ExecuteWithRegions(dataset);
    //        var outputMemoryStream = new MemoryStream();

    //        PDF.Save(outputMemoryStream, SaveFormat.Pdf);
            
            
    //        byte[] buffer = new byte[0];

    //        buffer = outputMemoryStream.ToArray();
    //        return buffer;
    //    }
    //}
    }
