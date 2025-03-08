using DocumentVerificationAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace DocumentVerificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        private readonly AppDbContext _db;
        public DocumentController(AppDbContext db)
        {
            _db = db;
            
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument(Document doc)
        {

        }
}
