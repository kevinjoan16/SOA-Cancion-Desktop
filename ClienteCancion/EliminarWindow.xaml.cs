﻿using System;
using System.Windows;
using Cancion = ClienteCancion.CancionSW.cancion;

namespace ClienteCancion
{
    /// <summary>
    /// Interaction logic for EliminarWindow.xaml
    /// </summary>
    public partial class EliminarWindow : Window
    {
        private CancionSW.CancionSWClient clienteCancion;
        private ViewModelCancion viewModel;

        public EliminarWindow(CancionSW.CancionSWClient clienteCancion)
        {
            this.clienteCancion = clienteCancion;
            InitializeComponent();
            viewModel = new ViewModelCancion();
            DataContext = viewModel;
        }

        private void BuscarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(BuscarTxt.Text.Trim(), out int id))
            {
                MessageBox.Show("Ingrese un id válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    Cancion cancion = clienteCancion.buscarCancion(id);
                    if (cancion == null)
                    {
                        MessageBox.Show("No se encontro una canción con el id dado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        viewModel.Cancion = cancion;
                    }
                }
                catch
                {
                    MessageBox.Show("No se encontro una canción con el id dado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void EliminarBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clienteCancion.eliminarCancion(viewModel.Id);
                MessageBox.Show("Eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                viewModel.Cancion = null;
            }
            catch
            {
                MessageBox.Show("Error al eliminar, tal vez ya alguien elimino la canción", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
