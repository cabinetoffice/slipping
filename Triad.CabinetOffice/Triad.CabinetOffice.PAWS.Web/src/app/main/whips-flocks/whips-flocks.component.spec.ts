import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WhipsFlocksComponent } from './whips-flocks.component';

describe('WhipsFlocksComponent', () => {
  let component: WhipsFlocksComponent;
  let fixture: ComponentFixture<WhipsFlocksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WhipsFlocksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WhipsFlocksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
