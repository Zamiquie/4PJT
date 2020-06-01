import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { AdvertComponent } from './advert.component';

describe('AdvertComponent', () => {
  let component: AdvertComponent;
  let fixture: ComponentFixture<AdvertComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvertComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(AdvertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
