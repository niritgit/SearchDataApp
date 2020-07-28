import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { QueryResult } from './queryResult.model';

@Injectable({ providedIn: 'root' })
export class DataStorageService {
  constructor(private http: HttpClient) { }

  //search a keyword in Google and Bing sites, get top 10 query results
  getResults(keyword: string) {
    return this.http.get<any[]>('/api/QueryResults/' + keyword).pipe(map(results => {
      return results.map(res => {
        return new QueryResult(res.title, res.searchEngineId==1? 'Google':'Bing')
      })
    }));

  }
}
