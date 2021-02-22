import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { IArticle } from "src/app/shared/models/article";
import { ArticleService } from "../article.service";

@Component({
  selector: "app-article-details",
  templateUrl: "./article-details.component.html",
  styleUrls: ["./article-details.component.scss"],
})
export class ArticleDetailsComponent implements OnInit {
  article: IArticle;

  constructor(
    private articleService: ArticleService,
    private activateRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    console.log(JSON.stringify("params:" + this.activateRoute.snapshot.params));
    let id = this.activateRoute.snapshot.params.id;
    console.log("id:" + id);
    this.articleService.getArticle(id).subscribe(
      (product) => {
        this.article = product;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
