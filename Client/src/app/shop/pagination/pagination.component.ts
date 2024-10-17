import { AfterViewChecked, AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent {

  @Input() PageCount: number = 100
  @Output() CurrentPage =new EventEmitter<number>();
  private _page: number = 1; // Default page value

  // Getter and setter for the page property
  get page(): number {
    return this._page;
  }

  set page(value: number) {
    this._page = value;
    this.onPageChange(value);  // Call the method when the page changes
  }

  // Method that gets called when page is changed
  onPageChange(page: number): void {
    this.CurrentPage.emit(page);

    // Your logic when the page changes, e.g., fetching data or other logic
  }



  selectPage(page: string) {

    this.page = parseInt(page, 10) || 1;
    this.onPageChange(this.page);
	}

	formatInput(input: HTMLInputElement) {
    input.value = input.value.replace(/[^0-9]/g, '');
    this.page = +input.value;
   this.onPageChange(this.page)
  }

}
