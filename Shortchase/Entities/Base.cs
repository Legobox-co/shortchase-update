using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortchase.Entities
{
    public partial interface IBase<T>
    {
        DateTime RowDate { get; set; }
        T Id { get; set; }
    }

    public class GuidBase : IBase<Guid>
    {
        public Guid Id { get; set; }
        public DateTime RowDate { get; set; }
    }

    public class IntBase : IBase<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime RowDate { get; set; }
    }
}