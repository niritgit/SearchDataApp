import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

import { DataStorageService } from './data-storage.service';
import { QueryResult } from './queryResult.model';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  @ViewChild('f', { static: false }) searchForm: NgForm;
  results: QueryResult[] = [];
  isLoading = false;

  constructor(private dataStorageService: DataStorageService) {
  }
  //search keyword by calling service, get top query results (in Google and Bing sites) and store them
  onSearch(keyword) {
    if (keyword.trim() == "") {
      return;
    }
    this.isLoading = true;
    this.dataStorageService.getResults(keyword).subscribe(
      data => {
        this.results = data;
        this.isLoading = false;
      },
      err => {
        alert(err.message);
        this.isLoading = false;
      }
    );
  }
}
