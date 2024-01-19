using Bancard.DAL.Models;

namespace Bancard.DAL
{
    public class FpgBancardDAL
    {
        public bool Save(FpgBancard op)
        {
            var res = false;
            try
            {
                using(var dbcontext = new DbsistemaCorisContext())
                {
                    dbcontext.FpgBancards.Add(op);
                    dbcontext.SaveChanges();
                    res = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return res;
        }


        public List<FpgBancard> Get(int transactionId)
        {
            var res = new List<FpgBancard>();
            try
            {
                using (var dbcontext = new DbsistemaCorisContext())
                {
                    res = dbcontext.FpgBancards.Where(x=>x.ShopProcessId.Equals(transactionId)).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return res;
        }
    }
}