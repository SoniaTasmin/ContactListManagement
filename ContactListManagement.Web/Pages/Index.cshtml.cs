using ContactListManagement.Core.Entities;
using ContactListManagement.Core.Interfaces;
using ContactListManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ContactListManagement.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IContactListRepositoryAsync _contactList;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRazorRenderService _renderService;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, IContactListRepositoryAsync contactList, IUnitOfWork unitOfWork, IRazorRenderService renderService)
        {
            _logger = logger;
            _contactList = contactList;
            _unitOfWork = unitOfWork;
            _renderService = renderService;
        }
        public IEnumerable<ContactList> ContactLists { get; set; }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            ContactLists = await _contactList.GetAllAsync();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<ContactList>>(ViewData, ContactLists)
            };
        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new ContactList()) });
            else
            {
                var thisContactList = await _contactList.GetByIdAsync(id);
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", thisContactList) });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, ContactList contactList)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _contactList.AddAsync(contactList);
                    await _unitOfWork.Commit();
                }
                else
                {
                    await _contactList.UpdateAsync(contactList);
                    await _unitOfWork.Commit();
                }
                ContactLists = await _contactList.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", ContactLists);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_CreateOrEdit", contactList);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var contactList = await _contactList.GetByIdAsync(id);
            await _contactList.DeleteAsync(contactList);
            await _unitOfWork.Commit();
            ContactLists = await _contactList.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", ContactLists);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}