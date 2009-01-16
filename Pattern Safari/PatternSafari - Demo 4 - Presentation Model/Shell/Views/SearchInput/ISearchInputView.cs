using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchApplication.Views.SearchInput
{
    public interface ISearchInputView
    {
        event SearchRequestEventHandler SearchRequested;
    }
}