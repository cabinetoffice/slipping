import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MembersOfParliamentComponent } from './members-of-parliament.component';

describe('MembersOfParliamentComponent', () => {
  let component: MembersOfParliamentComponent;
  let fixture: ComponentFixture<MembersOfParliamentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MembersOfParliamentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MembersOfParliamentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
