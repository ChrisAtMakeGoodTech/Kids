namespace Kids.Data.Models
{
	public partial class KidFamily
	{
		public int KidId { get; set; }
		public int FamilyId { get; set; }

		public virtual Family Family { get; set; }
		public virtual Kid Kid { get; set; }
	}
}
