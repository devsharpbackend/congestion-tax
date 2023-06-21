namespace Fintranet.BuildingBlocks.Common.Application.Models.ViewModel;

public class ViewModel
{

}
public class BaseViewModel:ViewModel
{
    public virtual DateTimeOffset? CreatedAt { get; set; }
    public virtual DateTimeOffset UpdatedAt { get; set; }
}

public class BaseEntityViewModel : BaseViewModel
{
  public string? Id { get; set; }
}
public class ViewListModel
{
    public long TotalCount { get; set; }
}
