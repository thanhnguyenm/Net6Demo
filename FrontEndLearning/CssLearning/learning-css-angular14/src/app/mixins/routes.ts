import { GridlayoutComponent } from './../gridlayout/gridlayout.component';
import { FlexboxComponent } from './../flexbox/flexbox.component';
import { BoxSizingComponent } from "../box-sizing/box-sizing.component";
import { CssSelectorComponent } from "../cssselector/cssselector.component";
import { HomeComponent } from "../home/home.component";
import { PseudoClassComponent } from "../pseudo-class/pseudo-class.component";
import { PseudoElementComponent } from "../pseudo-element/pseudo-element.component";
import { Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'box-sizing',
    component: BoxSizingComponent
  },
  {
    path: 'css-selector',
    component: CssSelectorComponent
  },
  {
    path: 'pseudo-class',
    component: PseudoClassComponent
  },
  {
    path: 'pseudo-element',
    component: PseudoElementComponent
  },
  {
    path: 'flex-box',
    component: FlexboxComponent
  },
  {
    path: 'grid-layour',
    component: GridlayoutComponent
  },
  {
    path: '**',
    redirectTo: '',
  }
];


export default routes;