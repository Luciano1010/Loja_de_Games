﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_loja_de_Games.Model
{
   
    public class User 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(255)]

        public string Nome { get; set; } = string.Empty;
        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Usuario { get; set; } = string.Empty;
        [Column(TypeName = "Varchar")]
        [StringLength(255)]

        public string Senha { get; set; } = string.Empty;
        [Column(TypeName = "Varchar")]
        [StringLength(255)]

        public string? Foto { get; set; } = string.Empty;
        [Column(TypeName = "Varchar")]
        [StringLength(5000)]

           

        [InverseProperty("Usuario")]
        public virtual ICollection<Produto>? Produto { get; set; }
       
    }







}
