using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlacToMp3ConverterV2.Model;
using FlacToMp3ConverterV2.View.FoldersViewer;

namespace FlacToMp3ConverterV2.View
{
    public static class ViewManager
    {
        public static FoldersView ShowFoldersView(Window owner, ModelService modelService)
        {
            FoldersView foldersView = new FoldersView(modelService)
            {
                Owner = owner,
            };

            foldersView.Show();

            return foldersView;
        }

        public static ChangeSelectedEntryPopUp ShowChangeSelectedEntryPopUp(Window owner, ModelService modelService)
        {
            ChangeSelectedEntryPopUp changeSelectedEntryPopUp = new ChangeSelectedEntryPopUp(modelService)
            {
                Owner = owner,
            };

            changeSelectedEntryPopUp.ShowDialog();

            return changeSelectedEntryPopUp;
        }
    }
}
