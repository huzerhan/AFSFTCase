using AFSFTCase.Data;
using AFSFTCase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace AFSFTCase.Controllers
{
    public class TranslationHistoryController: Controller
    {
        public AFSFTDbContext _db;
        public TranslationHistoryController(AFSFTDbContext db)
        {
            _db = db;
        }


        public IActionResult TranslationHistory()
        {
            IEnumerable<DataModelEntity> pastCalls = _db.DataModelEntities.OrderByDescending(x=> x.TranslationTime).ToList();
            return View(pastCalls);
        }


        public async Task<IActionResult> SortFilter(string? sortOrder,string dateSort, string searchString, string translator/*, DateTime? start, DateTime? end*/)
        {
            #region sorting

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewData["DateSortParm"] = String.IsNullOrEmpty(dateSort)? "" : "date_desc";
            
            #endregion sorting

            #region filtering

            ViewData["CurrentFilter"] = searchString;
            //ViewData["DateFilterStart"] = start; // I couldn't make filter based on date work. So I commented it out
            //ViewData["DateFilterEnd"] = end;
            ViewData["Translator"] = translator;

            #endregion filtering


            IQueryable<DataModelEntity> entries = from s in _db.DataModelEntities
                                                  select s;

            // this part turns date parametres to double and compares them to each other
            #region commented out
            //I couldn't make filter based on date work. So I commented it out
            //if (end.HasValue)
            //{
            //    end.
            //    entries = entries.Where(s => s.TranslationTime.CompareTo(end)>0);
            //}
            //if (start.HasValue)
            //{
            //    entries = entries.Where(s => s.TranslationTime.CompareTo(end) < 0);
            //}
            #endregion commented out

            switch (sortOrder)
            {
                case "name_desc":
                    entries = entries.OrderByDescending(s => s.InputString);
                    break;
                case "Date":
                    entries = entries.OrderBy(s => s.TranslationTime);
                    break;
                case "date_desc":
                    entries = entries.OrderByDescending(s => s.TranslationTime);
                    break;
                default:
                    entries = entries.OrderBy(s => s.InputString);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                entries = entries.Where(s => s.InputString.Contains(searchString));
            }
            /*
            switch (sortOrder)// using switch case is much more faster than if statements
            {
                case "":
                    switch (sortOrder)
                    {
                        case "input_desc":
                            entries = entries.OrderByDescending(x => x.InputString);
                            break;
                        
                        case "trans_desc":
                            entries = entries.OrderByDescending(x => x.Translator);
                            break;
                        case "trans_asc":
                            entries = entries.OrderBy(x => x.Translator);
                            break;
                        case "input_asc":
                            entries = entries.OrderBy(x => x.InputString);
                            break;
                    }
                    break;

                default:
                case "":
                    entries = entries.OrderBy(x => x.TranslationTime);
                    break;
                case "date_desc":
                    entries = entries.OrderByDescending(x => x.TranslationTime);
                    break;
            }*/
            
            return View("TranslationHistory", await entries.AsNoTracking().ToListAsync());
        }

        // this method didn't work because I couldn't pass null variables to the stored procedure from MVC
        /*
        public IActionResult Filter(string input, DateTime? tTMax, DateTime? tTMin, string trnsltr)
        {
            if (input == null) input = " ";
            if (trnsltr == null) trnsltr = " ";
            if (tTMax==null) tTMax = Convert.ToDateTime("01.01.2222");
            if (tTMin==null) tTMin = Convert.ToDateTime("01.01.1899");

            var inputString = new SqlParameter("@IS", input);
            var translationTimeMax = new SqlParameter("@TRTMAX", tTMax);
            var translationTimeMin = new SqlParameter("@TRTMIN", tTMin);
            var translator = new SqlParameter("@TRNSLTR", trnsltr);

            
            IEnumerable<DataModelEntity> result = _db.DataModelEntities.FromSqlRaw(
                "exec sp_Filter   @inputstring = @IS ,@translationTimeMax = @TRTMAX, @translationTimeMin = @TRTMIN, @translator = @TRNSLTR",
                inputString, translationTimeMax, translationTimeMin, translator).ToList();

            // this query also didn't work because I couldn't compare DateTime varibles from MVC and from SQL

            //var query = _db.DataModelEntities.Where(
            //    x => x.InputString.Contains(input) &&
            //    x.Translator == trnsltr &&
            //    x.TranslationTime.ToOADate() < tTMax.ToOADate() &&
            //    x.TranslationTime.ToOADate() > tTMin.ToOADate()).Select(x => new
            //    {
            //        x.InputString,
            //        x.TranslatedString,
            //        x.TranslationTime,
            //        x.Translator
            //    }).ToList();

            return View("TranslationHistory", result);
        }
        */
    }
}
