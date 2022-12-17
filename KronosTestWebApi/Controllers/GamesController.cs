using Aspose.Pdf;
using Kronos.TestTask.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using Kronos.TestTask.businessLayer;
using System.Linq;

namespace WebAPI1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGames GamesService;

        private readonly ILogger<GamesController> _logger;

        public GamesController(ILogger<GamesController> logger, IGames GamesService)
        {
            _logger = logger;
            this.GamesService = GamesService;
        }


        [HttpPost]
        public List<PdfResult> ProducePDF([FromBody] Game games)
        {
            List<PdfResult> DataList = GamesService.PostGamesList(games.Results);

            string dataDir = "";
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();
            Aspose.Pdf.Page p = pdfDocument.Pages.Add(); p.SetPageSize(1188, 1188);

            // Initializes a new instance of the Table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            { };
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

                if (i % 2 == 0)
                {
                    row2.BackgroundColor = Color.White;
                }
                if (i % 2 == 1)
                {
                    row2.BackgroundColor = Color.WhiteSmoke;
                }

                if (R1.Gamename == null)
                {

                    row2.Cells.Add("");

                }
                else
                {
                    row2.Cells.Add(R1.Gamename);

                }
                row2.Cells.Add("");
                row2.Cells.Add(R1.Hmteam.Name);

                row2.Cells.Add("   VS   ");
                row2.Cells.Add(R1.Awteam.Name);
                row2.Cells.Add(R1.Location.ToString());
                row2.Cells.Add(R1.Gamedate);
                row2.Cells.Add(R1.Time);
                row2.Cells.Add(R1.Gamedateend);


            }

            // Add table object to first page of input document
            pdfDocument.Pages[1].Paragraphs.Add(table);

            // Save updated document containing table object
            pdfDocument.Save(Path.Combine(dataDir, "document_with_table_out.pdf"));

            return DataList;

        }



    }

}
