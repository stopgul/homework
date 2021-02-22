import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ArticlesComponent } from "./content/articles/articles.component";
import { NotFoundComponent } from "./core/not-found/not-found.component";

const routes: Routes = [
  { path: "", component: ArticlesComponent, data: { breadcrumb: "Article" } },
  {
    path: "article",
    loadChildren: () =>
      import("./content/content.module").then((mod) => mod.ContentModule),
    data: { breadcrumb: "Article" },
  },
  {
    path: "not-found",
    component: NotFoundComponent,
    data: { breadcrumb: "Not Found" },
  },
  { path: "**", redirectTo: "not-found", pathMatch: "full" },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
