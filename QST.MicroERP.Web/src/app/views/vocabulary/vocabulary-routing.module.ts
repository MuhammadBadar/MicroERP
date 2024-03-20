import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageVocabularyComponent } from './manage-vocabulary/manage-vocabulary.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';

const routes: Routes = [
  {
    path: "mngVoc",
    component: ManageVocabularyComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.ManageVocabulary, ''] },
    pathMatch: "full"
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VocabularyRoutingModule { }
