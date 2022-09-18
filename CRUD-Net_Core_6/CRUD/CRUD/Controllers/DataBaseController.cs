using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// This is index web page: https://localhost:7076/swagger/index.html
/*
 * async Task非同步機制節省資料庫操作消耗的時間
 */
namespace CRUD.Controllers
{
    // 路徑: api/DataBase
    // [controller]為18行controller前的字串
    [Route("api/[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        //field: 使用到models裡面呼叫的DbContext來進行資料庫操作
        private readonly DataBaseContext DataBaseContext;

        //constructor: 建構子
        public DataBaseController(DataBaseContext DataBaseContext)
        {
            this.DataBaseContext = DataBaseContext;
        }

        // GET: http://localhost:7076/api/DataBase
        [HttpGet]
        public async Task<ActionResult<List<DataBaseContext>>> Get()// 無Id參數
        {
            return Ok(await DataBaseContext.Data.ToListAsync());// 取得所有資料庫的資料
        }

        // GET: http://localhost:7076/api/DataBase/ +Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<DataBaseContext>> Get(byte Id)// 帶Id參數
        {
            var Data = await DataBaseContext.Data.FindAsync(Id);//依照Id取得資料庫資料
            if (Data == null)
                return BadRequest("Data Id Not Found.");
            return Ok(Data);
        }

        // POST: http://localhost:7076/api/DataBase
        [HttpPost]
        public async Task<ActionResult<List<DataBaseContext>>> Post(Datum Data)
        {
            DataBaseContext.Data.Add(Data);// 新增Data一個欄位, Id 和 Date 資料庫會自動產生
            await DataBaseContext.SaveChangesAsync();// await 等待執行完再接續執行
            return Ok(await DataBaseContext.Data.ToListAsync());
        }

        // PUT: http://localhost:7076/api/DataBase
        // update <string>data where requested id is matched, with parameter
        [HttpPut("{Id}")]
        public async Task<ActionResult<List<DataBaseContext>>> Put(byte Id, Datum Request)// 帶Id參數
        {
            var Data = await DataBaseContext.Data.FindAsync(Id);//依照Id取得資料庫資料
            if (Data == null)
                return BadRequest("Data Id Not Found.");
            Data.Data = Request.Data;
            await DataBaseContext.SaveChangesAsync();// await 等待執行完再接續執行

            return Ok(await DataBaseContext.Data.ToListAsync());
        }

        // DELETE: http://localhost:7076/api/DataBase/ +Id
        // delete Data where requested id is matched, with parameter
        [HttpDelete("{Id}")]
        public async Task<ActionResult<DataBaseContext>> Delete(byte Id)
        {
            var Data = await DataBaseContext.Data.FindAsync(Id);//依照Id取得資料庫資料
            if (Data == null)
                return BadRequest("Data Id Not Found.");
            DataBaseContext.Data.Remove(Data);
            await DataBaseContext.SaveChangesAsync();// await 等待執行完再接續執行

            return Ok(await DataBaseContext.Data.ToListAsync());
        }
    }
}
