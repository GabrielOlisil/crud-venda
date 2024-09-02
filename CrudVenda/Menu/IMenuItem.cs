using System;

namespace CrudVenda.Menu;

public interface IMenuItem
{
    public bool IsMarked { get; set; }
    public void RenderTitle();
    public string ToString();

}
