using DabeaV2.Common.Enums;
using System;
using System.Collections.Generic;

namespace DabeaV2.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
        }

        public long Id { get; set; }
        public bool IsActive { get; set; }

    }

    public class Modification
    {
        public Modification()
        {
            ModificationItems = new HashSet<ModificationItem>();
        }

        public long Id { get; set; }

        public DateTime Date { set; get; }
        public EntityModificationType ModificationType { set; get; }

        public virtual ICollection<ModificationItem> ModificationItems { get; set; }

        public long BenutzerId { get; set; }
        public virtual Benutzer Benutzer { get; set; }

        public long? ChangedPersonId { get; set; }
        public virtual Person ChangedPerson { get; set; }

        public long? ChangedBenutzerId { get; set; }
        public virtual Benutzer ChangedBenutzer { get; set; }

        public long? ChangedKontaktId { get; set; }
        public virtual Kontakt ChangedKontakt { get; set; }

        public long? ChangedAdresseId { get; set; }
        public virtual Adresse ChangedAdresse { get; set; }

        public long? ChangedTraegerId { get; set; }
        public virtual Traeger ChangedTraeger { get; set; }
    }

    public class ModificationItem
    {
        public long Id { get; set; }

        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public long ModificationId { get; set; }
        public virtual Modification Modification { get; set; }
    }

    public class Adresse : BaseEntity, IModifiableEntity
    {
        public Adresse()
        {
            Modifications = new HashSet<Modification>();
        }

        public string Strasse { get; set; }
        public string Ort { get; set; }
        public string Plz { get; set; }
        public int Hausnummer { get; set; }
        public string HausnummerZusatz { get; set; }

        public long? PersonId { get; set; }
        public virtual Person Person { get; set; }

        public long? TraegerId { get; set; }
        public virtual Traeger Traeger { get; set; }

        public virtual ICollection<Modification> Modifications { get; set; }
    }

    public class Traeger : BaseEntity, IModifiableEntity
    {
        public Traeger()
        {
            Modifications = new HashSet<Modification>();
            Kontakte = new HashSet<Kontakt>();
            Adressen = new HashSet<Adresse>();
        }

        public string Name { get; set; }
        public string NameZusatz { get; set; }

        public virtual ICollection<Modification> Modifications { get; set; }
        public virtual ICollection<Kontakt> Kontakte { get; set; }
        public virtual ICollection<Adresse> Adressen { get; set; }
    }

    public class Person : BaseEntity, IModifiableEntity
    {
        public Person()
        {
            Modifications = new HashSet<Modification>();
            Benutzer = new HashSet<Benutzer>();
            Kontakte = new HashSet<Kontakt>();
            Adressen = new HashSet<Adresse>();
        }

        public string Name { get; set; }
        public string VorName { get; set; }

        public virtual ICollection<Modification> Modifications { get; set; }
        public virtual ICollection<Benutzer> Benutzer { get; set; }
        public virtual ICollection<Kontakt> Kontakte { get; set; }
        public virtual ICollection<Adresse> Adressen { get; set; }
    }

    public class Benutzer : BaseEntity, IModifiableEntity
    {
        public Benutzer()
        {
            Modifications = new HashSet<Modification>();
            OwnModifications = new HashSet<Modification>();
        }

        public virtual ICollection<Modification> Modifications { get; set; }
        public virtual ICollection<Modification> OwnModifications { get; set; }


        public bool IsExtern { get; set; }

        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string UserName { get; set; }
        public string Passwort { get; set; }

        public DateTime? LastLogin { set; get; }
    }

    public class Kontakt : BaseEntity, IModifiableEntity
    {
        public Kontakt()
        {
            Modifications = new HashSet<Modification>();
        }

        public virtual ICollection<Modification> Modifications { get; set; }

        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}
