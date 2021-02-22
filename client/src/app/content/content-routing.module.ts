import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ArticleDetailsComponent } from "./articles/article-details/article-details.component";
import { ArticlesComponent } from "./articles/articles.component";

const routes: Routes = [
  { path: "", component: ArticlesComponent },
  {
    path: ":id",
    component: ArticleDetailsComponent,
    data: { breadcrumb: { alias: "articleDetails" } },
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ContentRoutingModule {}
