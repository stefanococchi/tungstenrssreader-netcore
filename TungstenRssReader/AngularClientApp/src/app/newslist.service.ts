import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NewsItem } from './newsitem';

@Injectable()
export class NewslistService {
 
    constructor(private http:HttpClient) {}
 
    getNews() {
        return this.http.get<NewsItem[]>('http://localhost:60942/api/v1/news');
    }
}
