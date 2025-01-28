using AutoMapper;
using Data;
using Dto;
using Dto.Plan;
using Dto.PlanServices;
using Entities;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Api.Repositories
{
    public interface IPlanRepository : IBaseRepository<Plan>
    {
        Task<IEnumerable<PlanView>> GetAllAsGroupAsync( string langauge);
        void EventOccured(Plan plan, int number);
        Task<PlanView?> GetByIdAsync(string id,string lg);
    }
    
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        private readonly IMapper mapper;
        public PlanRepository(DataContext db,IMapper mapper):base(db)
        {
            mapper = mapper;
        }

        public async Task<PlanView?> GetByIdAsync(string id, string lg)
        {
            var item = await _dbSet.FindAsync(id);
             var pv = mapper.Map<PlanView>(new TranslationView<Plan>() { Value = item, LG = lg });

            return pv;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<PlanView>> GetAllAsGroupAsync(string lg="ar")
        {
            //var s= await db.Database.SqlQuery <PlanResponse> (@$"select ").ToListAsync();


            //return await _dbSet.Select<Plan,PlanView>(  p => 
            //  mapper.Map<PlanView>(new TranslationView<Plan>() { LG = lg ,Value=p})

            //).ToListAsync();
            return await _dbSet.Select<Plan, PlanView>(p => new PlanView()
            {
                Name = HelperTranslation.getTranslationValueByLG(p.ProductName, lg),
                Description = HelperTranslation.getTranslationValueByLG(p.Description, lg),
                Amount = p.Amount,
                Active = p.Active,

                PlanFeatures = p.PlanFeatures.Select<PlanFeature, PlanFeatureView>(f => new PlanFeatureView()
                {


                    Name = HelperTranslation.getTranslationValueByLG(f.Name, lg),
                    Description = HelperTranslation.getTranslationValueByLG(f.Description, lg)




                }).ToList()
            }).ToListAsync();

        }

        public void EventOccured(Plan plan, int number)
        {
            //plan.NumberRequests = number;
        }
    }
}