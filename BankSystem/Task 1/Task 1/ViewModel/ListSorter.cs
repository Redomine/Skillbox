using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Task_1.ViewModel
{
    public class ListSorter
    {
        private void Sort(string sortBy, ListSortDirection direction, ListView lvClients)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lvClients.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        public void SortListView(RoutedEventArgs e, GridViewColumnHeader _lastHeaderClicked, ListSortDirection _lastDirection, ListView lvClients, MainWindow mainWindow)
        {
            ListSortDirection direction;
            ResourceDictionary resources = new ResourceDictionary();
            if (e.OriginalSource is GridViewColumnHeader headerClicked)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction, lvClients);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    mainWindow._lastHeaderClicked = headerClicked;
                    mainWindow._lastDirection = direction;

                }
            }
        }

    }
}
