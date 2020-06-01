import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss'],
})
export class AdvertComponent implements OnInit {

  @Input() title: string;
  @Input() subtitle: string;
  @Input() description: string;
  @Input() photo: string;

  constructor() { }

  ngOnInit() {}



}
