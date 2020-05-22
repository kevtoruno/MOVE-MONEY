import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-order-new',
  templateUrl: './order-new.component.html',
  styleUrls: ['./order-new.component.scss']
})
export class OrderNewComponent implements OnInit {
  myControl = new FormControl();
  myControl2 = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions: Observable<string[]>;
  options2: string[] = ['Bull', 'Shit', 'Bye'];
  filteredOptions2: Observable<string[]>;
  value: boolean;
  constructor() { }

  ngOnInit() {
    this.filteredOptions2 = this.myControl2.valueChanges.pipe(
      startWith(''),
      map(value => this._filter2(value))
    );
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
    
  }

  changeState() {
    this.value = true;
  }
  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().indexOf(filterValue) === 0);
  }
  private _filter2(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options2.filter(option => option.toLowerCase().indexOf(filterValue) === 0);
  }
}
