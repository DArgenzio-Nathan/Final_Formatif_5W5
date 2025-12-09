import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shake, shakeX, tada } from 'ng-animate';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    animations: [
      trigger('shake', [transition(':decrement', useAnimation(shake, { params: { timing: 2 }}))]),
      trigger('bounce', [transition(':increment', useAnimation(bounce, { params: { timing: 4 }}))]),
      trigger('tada', [transition(':decrement', useAnimation(tada, { params: { timing: 3 }}))])
    ],
    standalone: true
})
export class AppComponent {
  title = 'ngAnimations';

  shakeV = 0;
  bounceV = 0;
  tadaV = 0;
  boucle = true;
  turn = false;

  constructor() {

  }

  animerUneFois() {
    this.shakeV--;
    this.bounceV++;
    this.tadaV--;
  }

  animerEnBoucle(){
    this.shakeV--;
    this.bounceV++;
    this.tadaV--;

    setTimeout(() => {
      if(this.boucle)
        this.animerEnBoucle();
    },4000);
  }

  turnMe(){
    this.turn = true;
    setTimeout(() => {this.turn = false;},2000);
  }
}
