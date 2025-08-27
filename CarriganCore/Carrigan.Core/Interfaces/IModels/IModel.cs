namespace Carrigan.Core.Interfaces.IModels;

public interface IModel
{
    public DateTime? LastUpdated { get; set; }
    /// <summary>
    /// Note: this interface meant for use with Entity Framework models. This will be of little interest to anyone besides myself.
    /// This a sort of validation that the  perquisites for saving the record have been met.
    /// While it could be used for a board form of validation, it is meant to server a more specific context.
    /// Notice that most classes just return true, but certain classes, ex Images and Files, use this to avoid saving "empty" records to the database.
    /// For example an image record that doesn't have a file should never be saved to the database, instead it should set to null before the parent record is saved.
    /// Also if an image record that already is in the database that has already been saved should likely be removed from the parent record and deleted from the database.
    /// However, the parent record's database logic bears the responsibility of check this method and determining that the file should be saved or deleted, 
    /// this model is only reporting if the conditions have been met. Though normally the parent record inherits this logic from the base class.
    /// </summary>
    /// <returns>true/false</returns>
    public bool CanSaveToDatabase();
}
