import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { IArticle } from "src/app/shared/models/article";
import { catchError, map } from "rxjs/operators";
import { empty, Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class ArticleService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getArticle(assetId: string): Observable<IArticle> {
    return this.http.get<IArticle>(this.baseUrl + "content/" + assetId).pipe(
      map((response) => {
        return response;
      })
    );
  }

  getArticles(): Observable<IArticle[]> {
    return this.http.get<IArticle[]>(this.baseUrl + "content").pipe(
      map((response) => {
        //console.log(`articles: ${JSON.stringify(response)}`);
        return response;
      }),
      catchError((err) => {
        console.log(`error: ${JSON.stringify(err)}`);
        return empty();
      })
    );
  }

  getLastArticles(): Observable<IArticle[]> {
    let params = new HttpParams();
    params = params.append("limit", "5");
    return this.http
      .get<IArticle[]>(this.baseUrl + "content", { params: params })
      .pipe(
        map((response) => {
          //console.log(`articles: ${JSON.stringify(response)}`);
          return response;
        }),
        catchError((err) => {
          console.log(`error: ${JSON.stringify(err)}`);
          return empty();
        })
      );
  }

  getArticlesByCategory(categoryName: string): Observable<IArticle[]> {
    console.log(`categoryName: ${categoryName}`);
    return this.http
      .get<IArticle[]>(
        `${this.baseUrl}content/GetArticlesByCategory/${categoryName}`
      )
      .pipe(
        map((response) => {
          //console.log(`articles: ${JSON.stringify(response)}`);
          return response;
        })
      );
  }

  getCategories(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl + "content/getCategories").pipe(
      map((response) => {
        //console.log(`category: ${JSON.stringify(response)}`);
        return response;
      }),
      catchError((err) => {
        console.log(`error: ${JSON.stringify(err)}`);
        return empty();
      })
    );
  }
}
