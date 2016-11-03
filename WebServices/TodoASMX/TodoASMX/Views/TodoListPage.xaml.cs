﻿using System;
using Xamarin.Forms;

namespace TodoASMX
{
	public partial class TodoListPage : ContentPage
	{
		bool alertShown = false;

		public TodoListPage ()
		{
			InitializeComponent ();
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();

			if (Constants.SoapUrl.Contains ("developer.xamarin.com")) {
				if (!alertShown) {
					await DisplayAlert (
						"Hosted Back-End",
						"This app is running against Xamarin's read-only ASMX service. To create, edit, and delete data you must update the service endpoint to point to your own hosted ASMX service.",
						"OK");
					alertShown = true;				
				}
			}

			listView.ItemsSource = await App.TodoManager.GetTodoItemsAsync ();
		}

		async void OnAddItemClicked (object sender, EventArgs e)
		{
			var todoItem = new TodoItem () {
				ID = Guid.NewGuid ().ToString ()
			};
			var todoPage = new TodoItemPage (true);
			todoPage.BindingContext = todoItem;
			await Navigation.PushAsync (todoPage);
		}

		async void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var todoItem = e.SelectedItem as TodoItem;
			var todoPage = new TodoItemPage ();
			todoPage.BindingContext = todoItem;
			await Navigation.PushAsync (todoPage);
		}
	}
}
