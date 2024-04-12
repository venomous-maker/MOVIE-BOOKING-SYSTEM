using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services;

public interface ISnackService:IEntityBaseRepository<Snack>
{
    Task<Snack> GetSnackByIdAsync(int id);
    Task AddNewSnackAsync(SnackVM data);
    Task UpdateSnackAsync(SnackVM data);
}