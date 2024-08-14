using System;

namespace CrudVenda.Control;

public interface IMenuItem
{
    public bool IsMarked { get; set; }
    public void RenderTitle();
    public string ToString();

}
